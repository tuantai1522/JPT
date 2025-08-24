export type LoginRequest = {
  email: string;
  password: string;
};

export type LoginResponse = {
  token: string;
  id: string;
  email: string;
  userRole: string;
};
