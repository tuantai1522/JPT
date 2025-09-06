import { useMutation } from "@tanstack/react-query";
import { logOut } from "../../apis/users/logout.api";
import type { AxiosError } from "axios";
import type { BaseError } from "../../types/baseError.types";

export const useLogOut = () =>
  useMutation<boolean, AxiosError<BaseError>, void>({
    mutationFn: logOut,
  });
