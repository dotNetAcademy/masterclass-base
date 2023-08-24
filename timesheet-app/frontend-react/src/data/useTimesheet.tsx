import useSWR from "swr";

export default function useTimesheet(userId: number, from: Date, until: Date) {
  console.log('Fetching with:', userId, from.toISOString(), until.toISOString());
  const { data, error, isLoading } = useSWR(`https://jsonplaceholder.typicode.com/posts?userId=${userId}&from=${from.toISOString()}&until=${until.toISOString()}`);

  return { data, error, isLoading };
}