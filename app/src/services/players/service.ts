import { ListResult, Result } from '../types';
import { BasicPlayerModel } from './types';

export class PlayersService {
  public async listPlayers(): Promise<ListResult<BasicPlayerModel>> {
    const response = await fetch('http://localhost:5000/players', {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
    });

    return await response.json();
  }

  public async getPlayer(id: string): Promise<Result<BasicPlayerModel>> {
    const response = await fetch(`http://localhost:5000/players/${id}`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
    });

    return await response.json();
  }
}

const Players = new PlayersService();

export default Players;
