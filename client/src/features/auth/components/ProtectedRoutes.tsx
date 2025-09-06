import { Navigate } from "@tanstack/react-router";
import { useGetCurrentUser } from "../../shared/hooks/queries/useGetCurrentUser";
import type { UserRole } from "../../shared/constants/userRole";
import type { PropsWithChildren } from "react";
import { useAuth } from "../contexts/useAuth";

type ProtectedRoutesProps = PropsWithChildren & {
  allowedRoles?: UserRole[];
};

export const ProtectedRoutes = ({
  allowedRoles,
  children,
}: ProtectedRoutesProps) => {
  const { token } = useAuth();

  // Navigate to "/" because of lack of token => don't log in (not error)
  if (!token) {
    return <Navigate to="/" />;
  }

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
