export interface IHbtSignalRMessage {
    type: string;
    title: string;
    content: string;
    timestamp: Date;
}

export interface IHbtSignalRClient {
    receiveMailStatus: (notification: IHbtSignalRMessage) => void;
    receiveNoticeStatus: (notification: IHbtSignalRMessage) => void;
    receiveTaskStatus: (notification: IHbtSignalRMessage) => void;
    receivePersonalNotice: (notification: IHbtSignalRMessage) => void;
    receiveBroadcast: (notification: IHbtSignalRMessage) => void;
    receiveHeartbeat: (timestamp: Date) => void;
    receiveKickout: (message: string) => void;
} 