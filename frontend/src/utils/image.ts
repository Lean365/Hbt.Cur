export interface ImageCompressOptions {
  quality?: number // 压缩质量，默认0.8
  maxWidth?: number // 最大宽度，默认1920
  maxHeight?: number // 最大高度，默认1080
  type?: string // 输出类型，默认保持原格式
}

export class ImageProcessor {
  private static instance: ImageProcessor
  private readonly defaultOptions: Required<ImageCompressOptions> = {
    quality: 0.8,
    maxWidth: 1920,
    maxHeight: 1080,
    type: 'image/jpeg'
  }

  private constructor() {}

  public static getInstance(): ImageProcessor {
    if (!ImageProcessor.instance) {
      ImageProcessor.instance = new ImageProcessor()
    }
    return ImageProcessor.instance
  }

  /**
   * 压缩图片
   * @param file 图片文件
   * @param options 压缩选项
   * @returns Promise<File>
   */
  public async compress(file: File, options?: ImageCompressOptions): Promise<File> {
    const finalOptions = {
      ...this.defaultOptions,
      type: file.type || this.defaultOptions.type,
      ...options
    }

    return new Promise((resolve, reject) => {
      const reader = new FileReader()
      reader.readAsDataURL(file)
      reader.onload = (e) => {
        const img = new Image()
        img.src = e.target?.result as string
        img.onload = () => {
          const canvas = document.createElement('canvas')
          let { width, height } = img
          
          // 计算缩放比例
          if (width > finalOptions.maxWidth || height > finalOptions.maxHeight) {
            const ratio = Math.min(
              finalOptions.maxWidth / width,
              finalOptions.maxHeight / height
            )
            width *= ratio
            height *= ratio
          }

          canvas.width = width
          canvas.height = height
          const ctx = canvas.getContext('2d')
          ctx?.drawImage(img, 0, 0, width, height)

          // 转换为Blob
          canvas.toBlob(
            (blob) => {
              if (blob) {
                const newFile = new File([blob], file.name, {
                  type: finalOptions.type,
                  lastModified: Date.now()
                })
                resolve(newFile)
              } else {
                reject(new Error('图片压缩失败'))
              }
            },
            finalOptions.type,
            finalOptions.quality
          )
        }
        img.onerror = () => reject(new Error('图片加载失败'))
      }
      reader.onerror = () => reject(new Error('图片读取失败'))
    })
  }

  /**
   * 获取图片的Base64编码
   * @param file 图片文件
   * @returns Promise<string>
   */
  public getBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader()
      reader.readAsDataURL(file)
      reader.onload = () => resolve(reader.result as string)
      reader.onerror = (error) => reject(error)
    })
  }
}

export default ImageProcessor.getInstance()