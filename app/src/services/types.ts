export type Result<T> = {
  data: T;
  messages: Message[];
  isSuccess: boolean;
};

export type ListResult<T> = Result<T[]>;

export type Position = [number, number];

export type Message = {
  type: MessageType;
  content: string;
};

export type MessageType = 'INFO' | 'WARNING' | 'ERROR';

export type Color = 'BLACK' | 'WHITE';

export type BoardSize = 64 | 100;
