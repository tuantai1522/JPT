import type { UserRole } from "../../constants/userRole";

export type GetCurrentUserResponse = {
  id: string;
  firstName: string;
  role: UserRole;
  emaiL: string;
  avatarUrl?: string;
  companyName?: string;
};
