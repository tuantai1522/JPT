import { differenceInHours, differenceInDays } from "date-fns";

export function formatTime(millis: string | number) {
  const date = new Date(Number(millis));
  const now = new Date();

  const hours = differenceInHours(now, date);

  if (hours < 24) {
    return `${hours} giờ trước`;
  }

  const days = differenceInDays(now, date);
  return `${days} ngày trước`;
}
