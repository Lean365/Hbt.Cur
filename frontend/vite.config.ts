import { fileURLToPath, URL } from 'node:url'
import { defineConfig, loadEnv, ConfigEnv, UserConfig, Plugin } from 'vite'
import vue from '@vitejs/plugin-vue'
import AutoImport from 'unplugin-auto-import/vite'
import Components from 'unplugin-vue-components/vite'
import { AntDesignVueResolver } from 'unplugin-vue-components/resolvers'
import * as http from 'node:http'
import * as https from 'node:https'

// 定义支持的语言
const SUPPORTED_LOCALES = {
  'zh-CN': {
    title: '\n  黑冰台前端开发服务器启动\n',
    separator: '  ' + '='.repeat(50),
    backendNotRunning: '  ❌ 后端服务未启动',
    backendRunning: '  ✅ 后端服务已连接',
    backendUrl: (url: string) => `  📡 后端服务地址: ${url}`,
    startBackend: '  💡 请先启动后端服务',
    continueInfo: '  ℹ️ 前端服务继续运行中，但部分功能可能无法使用',
    readyInfo: '  ✨ 所有服务已就绪，可以开始使用了',
    localUrl: (url: string) => `  🌐 本地访问: ${url}`,
    networkUrl: (url: string) => `  🔗 网络访问: ${url}`,
    helpInfo: '  ❓ 按 h + enter 显示帮助信息\n'
  },
  'en-US': {
    title: '\n  Lean.Hbt Frontend Dev Server Started\n',
    separator: '  ' + '='.repeat(50),
    backendNotRunning: '  ❌ Backend Service Not Running',
    backendRunning: '  ✅ Backend Service Connected',
    backendUrl: (url: string) => `  📡 Backend URL: ${url}`,
    startBackend: '  💡 Please Start Backend Service First',
    continueInfo: '  ℹ️ Frontend Service Continues, But Some Features May Not Work',
    readyInfo: '  ✨ All Services Ready, You Can Start Using Now',
    localUrl: (url: string) => `  🌐 Local: ${url}`,
    networkUrl: (url: string) => `  🔗 Network: ${url}`,
    helpInfo: '  ❓ Press h + enter for help\n'
  }
}

// 创建后端状态检测插件
function backendStatusPlugin(proxyTarget: string, locale: string = 'zh-CN'): Plugin {
  // 获取当前语言的消息
  const messages = SUPPORTED_LOCALES[locale] || SUPPORTED_LOCALES['zh-CN']
  
  const printStatus = (isRunning: boolean) => {
    console.clear() // 清除之前的输出
    console.log('\x1b[36m%s\x1b[0m', messages.title)
    console.log('\x1b[36m%s\x1b[0m', messages.separator)
    if (isRunning) {
      console.log('\x1b[32m%s\x1b[0m', messages.backendRunning)
      console.log('\x1b[33m%s\x1b[0m', messages.backendUrl(proxyTarget))
      console.log('\x1b[32m%s\x1b[0m', messages.readyInfo)
    } else {
      console.log('\x1b[31m%s\x1b[0m', messages.backendNotRunning)
      console.log('\x1b[33m%s\x1b[0m', messages.backendUrl(proxyTarget))
      console.log('\x1b[33m%s\x1b[0m', messages.startBackend)
      console.log('\x1b[36m%s\x1b[0m', messages.continueInfo)
    }
    console.log('\x1b[36m%s\x1b[0m', messages.separator)
    console.log('\x1b[32m%s\x1b[0m', messages.localUrl('http://localhost:5349/'))
    console.log('\x1b[32m%s\x1b[0m', messages.networkUrl('http://192.168.16.16:5349/'))
    console.log('\x1b[36m%s\x1b[0m', messages.helpInfo)
  }

  const checkBackendStatus = async (server?: any) => {
    try {
      const controller = new AbortController();
      const timeoutId = setTimeout(() => controller.abort(), 2000);
      
      return new Promise((resolve) => {
        const url = new URL(proxyTarget);
        const options = {
          hostname: url.hostname,
          port: url.port,
          path: '/api/HbtLanguage/supported',
          method: 'GET',
          headers: {
            'Accept': 'application/json'
          },
          rejectUnauthorized: false,
          agent: new (url.protocol === 'https:' ? https : http).Agent({
            rejectUnauthorized: false
          })
        };

        const req = (url.protocol === 'https:' ? https : http).request(options, (res) => {
          clearTimeout(timeoutId);
          let data = '';
          
          res.on('data', (chunk) => {
            data += chunk;
          });
          
          res.on('end', () => {
            // 任何响应都表示后端在运行
            const isRunning = res.statusCode !== undefined;
            printStatus(isRunning);
            resolve(isRunning);
          });
        });

        req.on('error', (error) => {
          clearTimeout(timeoutId);
          console.log('\x1b[33m[Backend Check]', error.message, '\x1b[0m');
          printStatus(false);
          resolve(false);
        });

        req.end();
      });
    } catch (error) {
      console.log('\x1b[33m[Backend Check] Error:', error, '\x1b[0m');
      printStatus(false);
      return false;
    }
  }

  let checkInterval: NodeJS.Timeout | null = null

  return {
    name: 'backend-status',
    configureServer(server) {
      // 初始检查
      checkBackendStatus(server)

      // 定期检查后端状态
      checkInterval = setInterval(checkBackendStatus, 30000) // 每30秒检查一次

      // 服务关闭时清除定时器
      server.httpServer?.on('close', () => {
        if (checkInterval) {
          clearInterval(checkInterval)
        }
      })

      // 代理错误处理中间件
      server.middlewares.use((req, res, next) => {
        if (req.url === '/__backend_status') {
          checkBackendStatus(server).then(isRunning => {
            res.end(isRunning ? 'Backend is running' : 'Backend is not running')
          })
        } else {
          next()
        }
      })
    }
  }
}

// https://vitejs.dev/config/
export default defineConfig(({ mode }: ConfigEnv): UserConfig => {
  const env = loadEnv(mode, process.cwd())
  
  // 从环境变量或系统设置获取语言设置
  const locale = process.env.LOCALE || Intl.DateTimeFormat().resolvedOptions().locale
  
  return {
    plugins: [
      vue(),
      backendStatusPlugin(env.VITE_PROXY_TARGET, locale),
      AutoImport({
        imports: [
          'vue',
          'vue-router',
          'vue-i18n',
          'pinia',
          '@vueuse/core',
          {
            'ant-design-vue': [
              'message',
              'Modal',
              'notification'
            ]
          }
        ],
        dts: 'src/auto-imports.d.ts',
        dirs: ['src/composables', 'src/stores'],
        vueTemplate: true,
        defaultExportByFilename: true,
        eslintrc: {
          enabled: true,
        }
      }),
      Components({
        resolvers: [
          AntDesignVueResolver({
            importStyle: 'less',
            resolveIcons: true
          })
        ],
        dirs: ['src/components'],
        dts: 'src/components.d.ts',
      })
    ],
    css: {
      preprocessorOptions: {
        less: {
          javascriptEnabled: true
        }
      }
    },
    resolve: {
      alias: {
        '@': fileURLToPath(new URL('./src', import.meta.url))
      }
    },
    optimizeDeps: {
      include: [
        'vue',
        'vue-router',
        'pinia',
        '@vueuse/core',
        'ant-design-vue',
        '@ant-design/icons-vue'
      ],
      exclude: []
    },
    server: {
      host: '0.0.0.0',
      port: Number(env.VITE_PORT) || 5349,
      https: null,
      proxy: {
        '/api': {
          target: env.VITE_PROXY_TARGET,
          changeOrigin: true,
          secure: false,
          ws: true,
          rewrite: (path) => path.replace(/^\/api/, '/api'),
          configure: (proxy, options) => {
            proxy.on('error', (err: any) => {
              console.log('\n')
              console.log('\x1b[31m[vite] 后端服务连接失败\x1b[0m')
              console.log('\x1b[31m[vite] 错误信息: ' + err.code + '\x1b[0m')
              console.log('\x1b[33m[vite] 请检查后端服务是否已启动\x1b[0m')
              console.log('\x1b[33m[vite] 后端服务地址: ' + env.VITE_PROXY_TARGET + '\x1b[0m')
              console.log('\n')
            })
          }
        }
      },
      fs: {
        strict: true,
        allow: ['..']
      }
    },
    build: {
      outDir: 'dist',
      assetsDir: 'assets',
      sourcemap: false,
      chunkSizeWarningLimit: 1500,
      rollupOptions: {
        output: {
          manualChunks: {
            vue: ['vue', 'vue-router', 'pinia', '@vueuse/core'],
            antd: ['ant-design-vue', '@ant-design/icons-vue']
          }
        }
      }
    }
  }
})