import { z } from "zod";
import { logInFormSchema, registerFormSchema } from "./schema";

export type LogInForm = z.infer<typeof logInFormSchema>;
export type RegisterForm = z.infer<typeof registerFormSchema>;

export type LoginRequest = {
  email: string;
  password: string;
};

export type LoginResponse = {
  token: string;
  id: string;
  email: string;
  userRole: string;
};
