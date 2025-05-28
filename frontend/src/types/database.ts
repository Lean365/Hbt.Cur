export type DatabaseType = 'mysql' | 'sqlserver' | 'postgresql' | 'oracle' | 'sqlite'

export interface DatabaseConnection {
  dbType: DatabaseType
  host: string
  port: number
  database: string
  username: string
  password: string
  options?: string
} 