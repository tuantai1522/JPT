import { Navigate } from "@tanstack/react-router";
import { useGetCurrentUser } from "../../shared/hooks/queries/useGetCurrentUser";
import type { UserRole } from "../../shared/types/users/userRole";
import type { PropsWithChildren } from "react";

type ProtectedRoutesProps = PropsWithChildren & {
  allowedRoles?: UserRole[];
};

export const ProtectedRoutes = ({
  allowedRoles,
  children,
}: ProtectedRoutesProps) => {
  const { data, isLoading, isError } = useGetCurrentUser();

  if (isLoading) {
    return <div>Loading in protected routes</div>;
  }

  if (isError || !data) {
    return <Navigate to="/unauthorized" />;
  }

  if (allowedRoles && !allowedRoles.includes(data.role)) {
    return <Navigate to="/unauthorized" />;
  }

  return children;
};
