export type BaseError = {
  detail: string;
  status: number;
  errors: Error[];
};

type Error = {
  code: string;
  description: string;
  type: number;
};
