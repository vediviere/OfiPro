export interface Rating {
  ratingId: string;
  contractId: string;
  raterUserId: string;
  raterUserName: string;
  ratedUserId: string;
  ratedUserName: string;
  score: number;
  comment: string;
  createdAt: string;
}

export interface CreateRatingRequest {
  score: number;
  comment: string;
}

export const ratingScoreOptions = [
  { value: 5, label: '5 - Excelente' },
  { value: 4, label: '4 - Muy bueno' },
  { value: 3, label: '3 - Regular' },
  { value: 2, label: '2 - Malo' },
  { value: 1, label: '1 - Muy malo' },
];
