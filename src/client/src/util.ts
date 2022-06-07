export function randomInt(min: number, max: number): number {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

export function toTimeString(minutes: Number, seconds: Number): string {
  if (minutes < 10 && seconds < 10) {
    return `0${minutes}:0${seconds}`;
  }

  if (minutes < 10 && seconds >= 10) {
    return `0${minutes}:${seconds}`;
  }

  if (minutes >= 10 && seconds < 10) {
    return `${minutes}:0${seconds}`;
  }

  return `${minutes}:${seconds}`;
}
