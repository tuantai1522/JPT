import { useMutation } from "@tanstack/react-query";
import type { AxiosError } from "axios";
import type { BaseError } from "../../../shared/types/baseError";
import type { RegisterRequest } from "../../types/register";
import { register } from "../../apis/api.register";

export const useRegister = () =>
  useMutation<string, AxiosError<BaseError>, RegisterRequest>({
    mutationFn: register,
  });
