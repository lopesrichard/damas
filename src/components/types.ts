export type SlotProps = {
  id: string;
  position: [number, number];
  playable: boolean;
  hilighted: boolean;
  piece: PieceProps | null;
  onClick?: () => void;
};

export type BlackOrWhite = 'black' | 'white';

export type PieceProps = {
  id: string;
  color: BlackOrWhite;
  isDama: boolean;
  selected: boolean;
  hilighted: boolean;
};
