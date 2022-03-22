import { NewMatchModel, BasicMatchModel } from './types';
import { ListResult, Result } from '../types';
import { NewMoveModel } from '../moves/types';

export class MatchesService {
  public async newMatch(payload: NewMatchModel): Promise<Result<BasicMatchModel>> {
    const response = await fetch('http://localhost:5000/matches', {
      method: 'POST',
      body: JSON.stringify(payload),
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
    });

    return await response.json();
  }

  public async listPossibleMoves(id: string): Promise<ListResult<NewMoveModel>> {
    const response = await fetch(`http://localhost:5000/matches/${id}/moves/possibles`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
    });

    return await response.json();
  }
}

const Matches = new MatchesService();

export default Matches;
