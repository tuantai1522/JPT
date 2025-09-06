import { LayoutDashboard, Plus, Briefcase, Building2 } from "lucide-react";

export const NAVIGATION_MENU: {
  id: string;
  name: string;
  icon: React.ReactNode;
}[] = [
  { id: "/employers", name: "Dashboard", icon: <LayoutDashboard /> },
  { id: "post-job", name: "Post job", icon: <Plus /> },
  { id: "manage-jobs", name: "Manage jobs", icon: <Briefcase /> },
  { id: "company-profile", name: "Company profile", icon: <Building2 /> },
];
