import { useMutation } from "@tanstack/react-query";
import { login } from "../../apis/login.api";
import type { LoginRequest, LoginResponse } from "../../types/login.types";
import type { AxiosError } from "axios";
import type { BaseError } from "../../../shared/types/baseError.types";

export const useLogin = () =>
  useMutation<LoginResponse, AxiosError<BaseError>, LoginRequest>({
    mutationFn: login,
  });
