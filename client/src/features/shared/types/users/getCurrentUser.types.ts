import type { UserRole } from "../../constants/userRole";

export type GetCurrentUserResponse = {
  id: string;
  firstName: string;
  role: UserRole;
  avatarUrl?: string;
};
