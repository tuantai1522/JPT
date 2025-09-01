import { z } from "zod";

export const logInFormSchema = z.object({
  email: z.email(),
  password: z
    .string()
    .min(8, "Password must be at least 8 characters")
    .max(128),
});

export type LogInFormSchema = z.infer<typeof logInFormSchema>;
