import type { UserRole } from "./userRole";

export type User = {
  id: string;
  firstName: string;
  role: UserRole;
};