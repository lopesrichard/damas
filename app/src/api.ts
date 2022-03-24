export type Result<T> = {
  data: T;
  messages: Message[];
  isSuccess: boolean;
};

export type ListResult<T> = Result<T[]>;

export type Message = {
  type: MessageType;
  content: string;
};

export type MessageType = 'INFO' | 'WARNING' | 'ERROR';

export type Color = 'BLACK' | 'WHITE';

export type Position = { x: number; y: number };

export type NewMatchModel = {
  playerOneId: string;
  playerOneColor: Color;
  playerTwoId: string;
  playerTwoColor: Color;
  boardSize: number;
};

export interface BasicMatchModel {
  id: string;
  playerOneId: string;
  playerOneColor: Color;
  playerTwoId: string;
  playerTwoColor: Color;
  whoseTurn: Color;
  boardSize: number;
  winnerId?: string;
  isDraw: boolean;
  startedAt: Date;
  finishedAt?: Date;
  moves: number;
  pieces: BasicPieceModel[];
}

export type BasicPieceModel = {
  id: string;
  position: Position;
  color: Color;
  isDama: boolean;
  isCaptured: boolean;
};

export type NewMoveModel = {
  pieceId: string;
  newPosition: Position;
};

export type BasicMoveModel = {
  id: string;
  pieceId: string;
  previousPosition: Position;
  newPosition: Position;
  capturedPieceId?: string;
  isPromotionMove: boolean;
  dateTime: Date;
};

export type BasicPlayerModel = {
  id: string;
  name: string;
};

export class Api {
  private headers: HeadersInit;

  constructor() {
    this.headers = {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    };
  }

  public async newMatch(payload: NewMatchModel) {
    return await this.post<Result<BasicMatchModel>>('matches', payload);
  }

  public async move(payload: NewMoveModel) {
    return await this.post<Result<BasicMoveModel>>(`moves`, payload);
  }

  public async listPossibleMoves(id: string) {
    return await this.get<ListResult<NewMoveModel>>(`matches/${id}/moves/possibles`);
  }

  public async listPlayers() {
    return await this.get<ListResult<BasicPlayerModel>>('players');
  }

  private async get<T>(path: string) {
    return await this.request<T>(path, 'GET');
  }

  private async post<T>(path: string, payload: unknown) {
    return await this.request<T>(path, 'POST', payload);
  }

  private async put<T>(path: string, payload: unknown) {
    return await this.request<T>(path, 'PUT', payload);
  }

  private async request<T>(path: string, method: 'GET' | 'POST' | 'PUT', payload?: unknown) {
    return await this.fetch<T>(path, {
      method: method,
      body: payload ? JSON.stringify(payload) : null,
      headers: this.headers,
    });
  }

  private async fetch<T>(path: string, request: RequestInit): Promise<T> {
    const endpoint = this.endpoint(path);
    const response = await fetch(endpoint, request);
    return await response.json();
  }

  private endpoint(path: string) {
    return `http://localhost:5000/${path}`;
  }
}

const api = new Api();

export default api;
