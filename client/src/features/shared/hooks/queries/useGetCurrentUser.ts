import { useQuery } from "@tanstack/react-query";
import type { GetCurrentUserResponse } from "../../types/users/getCurrentUser.types";
import { getCurrentUser } from "../../apis/users/getCurrentUser.api";

export function useGetCurrentUser() {
  return useQuery<GetCurrentUserResponse>({
    queryKey: ["me"],
    queryFn: getCurrentUser,
    staleTime: 5 * 60 * 1000,
    retry: 1,
  });
}
