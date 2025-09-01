export type RegisterRequest = {
  firstName: string;
  middleName?: string;
  lastName?: string;
  email: string;
  password: string;
  avatarId?: string;
  role: string;
  companyName: string;
  logoId?: string;
};
