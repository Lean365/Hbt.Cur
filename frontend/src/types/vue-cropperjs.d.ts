declare module 'vue-cropperjs' {
  import { DefineComponent } from 'vue'

  export interface VueCropperInstance {
    rotate(degree: number): void
    zoom(ratio: number): void
    reset(): void
    getCroppedCanvas(options?: any): HTMLCanvasElement
    replace(url: string): void
    getContainerData(): { width: number; height: number }
    setCropBoxData(data: { left: number; top: number; width: number; height: number }): void
    destroy(): void
    getData(): { scaleX: number; scaleY: number }
    scale(ratio: number): void
  }

  const VueCropper: DefineComponent<{
    src: string
    options: any
  }>

  export default VueCropper
}

export {} 