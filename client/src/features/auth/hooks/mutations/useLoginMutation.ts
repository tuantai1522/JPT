import { useMutation } from "@tanstack/react-query";
import { postLogin } from "../../apis/api.auth";
import type { LoginResponse, LoginPayload } from "../../type";

export const useLoginMutation = () =>
  useMutation<LoginResponse, unknown, LoginPayload>({
    mutationFn: postLogin,
  });
