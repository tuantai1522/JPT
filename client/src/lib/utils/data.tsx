import {
  LayoutDashboard,
  Plus,
  Briefcase,
  Building2,
  Users,
  CheckCircle2,
} from "lucide-react";

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

export const EMPLOYER_DASHBOARD: {
  title: string;
  icon: React.ReactNode;
  value?: string;
  color: "blue" | "green" | "purple" | "orange";
  key: string;
}[] = [
  {
    title: "Active jobs",
    icon: <LayoutDashboard className="h-6 w-6" />,
    color: "blue",
    key: "activeJobs",
  },
  {
    title: "Total applicants",
    icon: <Users className="h-6 w-6" />,
    color: "green",
    key: "totalApplicants",
  },
  {
    title: "Hired",
    icon: <CheckCircle2 className="h-6 w-6" />,
    color: "purple",
    key: "hired",
  },
];
