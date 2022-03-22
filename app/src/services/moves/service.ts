import { BasicMoveModel, NewMoveModel } from './types';
import { Result } from '../types';

export class MovesService {
  public async newMove(payload: NewMoveModel): Promise<Result<BasicMoveModel>> {
    const response = await fetch('http://localhost:5000/moves', {
      method: 'POST',
      body: JSON.stringify(payload),
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
    });

    return await response.json();
  }
}

const Moves = new MovesService();

export default Moves;
