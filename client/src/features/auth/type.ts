import { z } from "zod";
import { logInFormSchema, registerFormSchema } from "./schema";

export type LogInFormSchema = z.infer<typeof logInFormSchema>;
export type RegisterFormSchema = z.infer<typeof registerFormSchema>;
