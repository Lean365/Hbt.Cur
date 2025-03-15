import SparkMD5 from 'spark-md5'

export interface ChunkInfo {
  chunk: Blob
  filename: string
  hash: string
  index: number
}

export interface UploadConfig {
  chunkSize?: number // 分块大小，默认2MB
  threads?: number // 并发上传数，默认3
  retryCount?: number // 重试次数，默认3
  retryDelay?: number // 重试延迟，默认1000ms
}

export class FileUploader {
  private static instance: FileUploader
  private readonly defaultConfig: Required<UploadConfig> = {
    chunkSize: 2 * 1024 * 1024,
    threads: 3,
    retryCount: 3,
    retryDelay: 1000
  }

  private constructor(private config: Required<UploadConfig>) {}

  public static getInstance(config?: UploadConfig): FileUploader {
    if (!FileUploader.instance) {
      FileUploader.instance = new FileUploader({
        ...new FileUploader(null as any).defaultConfig,
        ...config
      })
    }
    return FileUploader.instance
  }

  /**
   * 计算文件MD5
   * @param file 文件对象
   * @returns Promise<string> MD5值
   */
  public async calculateFileMD5(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const blobSlice = File.prototype.slice
      const chunks = Math.ceil(file.size / this.config.chunkSize)
      let currentChunk = 0
      const spark = new SparkMD5.ArrayBuffer()
      const fileReader = new FileReader()

      fileReader.onload = (e: ProgressEvent<FileReader>) => {
        if (e.target?.result instanceof ArrayBuffer) {
          spark.append(e.target.result)
          currentChunk++

          if (currentChunk < chunks) {
            loadNext()
          } else {
            resolve(spark.end())
          }
        }
      }

      fileReader.onerror = reject

      const loadNext = () => {
        const start = currentChunk * this.config.chunkSize
        const end = start + this.config.chunkSize >= file.size ? file.size : start + this.config.chunkSize
        fileReader.readAsArrayBuffer(blobSlice.call(file, start, end))
      }

      loadNext()
    })
  }

  /**
   * 创建文件分块
   * @param file 文件对象
   * @returns ChunkInfo[] 分块信息数组
   */
  public createFileChunks(file: File): ChunkInfo[] {
    const chunks: ChunkInfo[] = []
    let cur = 0
    while (cur < file.size) {
      chunks.push({
        chunk: file.slice(cur, cur + this.config.chunkSize),
        filename: file.name,
        hash: '', // 由上传者设置完整hash
        index: Math.floor(cur / this.config.chunkSize)
      })
      cur += this.config.chunkSize
    }
    return chunks
  }

  /**
   * 上传分块
   * @param chunks 分块信息数组
   * @param uploadUrl 上传地址
   * @param headers 请求头
   * @param onProgress 进度回调
   * @param uploadedChunks 已上传的分块集合
   * @returns Promise<void>
   */
  public async uploadChunks(
    chunks: ChunkInfo[],
    uploadUrl: string,
    headers: Record<string, string>,
    onProgress?: (progress: number) => void,
    uploadedChunks: Set<string> = new Set()
  ): Promise<void> {
    const pendingChunks = chunks.filter(chunk => !uploadedChunks.has(chunk.hash))
    const total = pendingChunks.length
    let completed = 0

    // 分组上传，控制并发数
    const groups = this.groupChunks(pendingChunks, this.config.threads)
    
    for (const group of groups) {
      await Promise.all(
        group.map(async chunk => {
          let retries = 0
          while (retries < this.config.retryCount) {
            try {
              const formData = new FormData()
              formData.append('chunk', chunk.chunk)
              formData.append('hash', chunk.hash)
              formData.append('filename', chunk.filename)
              formData.append('index', chunk.index.toString())

              const response = await fetch(uploadUrl, {
                method: 'POST',
                headers,
                body: formData
              })

              if (!response.ok) {
                throw new Error(`上传失败: ${response.statusText}`)
              }

              uploadedChunks.add(chunk.hash)
              completed++
              onProgress?.(Math.floor((completed / total) * 100))
              break
            } catch (error) {
              retries++
              if (retries === this.config.retryCount) {
                throw error
              }
              await this.delay(this.config.retryDelay)
            }
          }
        })
      )
    }
  }

  /**
   * 合并请求
   * @param uploadUrl 上传地址
   * @param headers 请求头
   * @param params 合并参数
   * @returns Promise<Response>
   */
  public async mergeChunks(
    uploadUrl: string,
    headers: Record<string, string>,
    params: {
      filename: string
      size: number
      chunks: number
    }
  ): Promise<Response> {
    return fetch(`${uploadUrl}/merge`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...headers
      },
      body: JSON.stringify(params)
    })
  }

  private groupChunks<T>(chunks: T[], size: number): T[][] {
    const groups: T[][] = []
    for (let i = 0; i < chunks.length; i += size) {
      groups.push(chunks.slice(i, i + size))
    }
    return groups
  }

  private delay(ms: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms))
  }
}

export default FileUploader.getInstance() 