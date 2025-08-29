import { useQuery } from "@tanstack/react-query";
import type { GetCurrentUserResponse } from "../../types/users/getCurrentUser";
import { getCurrentUser } from "../../apis/users/api.getCurrentUser";

export function useGetCurrentUser() {
  return useQuery<GetCurrentUserResponse>({
    queryKey: ["me"],
    queryFn: getCurrentUser,
    staleTime: 5 * 60 * 1000,
    retry: 1,
  });
}
