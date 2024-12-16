import { ky } from "@/lib/ky";
import {
  queryOptions,
  useMutation,
  useQuery,
  useQueryClient,
} from "@tanstack/react-query";

import type { SafeUser } from "@/interfaces/users.interface";
import type { LoginSchema } from "@/lib/schema";

export const authQueryOptions = () => {
  return queryOptions({
    queryKey: ["auth"],
    queryFn: () => ky.get("weatherforecast").json<SafeUser>(),
    // queryFn: () => ky.get("auth").json<SafeUser>(),
  });
};

export const useAuthQuery = () => {
  return useQuery(authQueryOptions());
};

export const useLoginMutation = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["sign-in"],
    mutationFn: async (userData: LoginSchema) => {
      return ky
        .post("auth/login", {
          json: userData,
        })
        .json<SafeUser>();
    },
    onSuccess: (data) => {
      queryClient.setQueryData(["auth"], { user: data });
      // sessionStorage.setItem(ACCESS_TOKEN, data.accessToken)
    },
    onError: async (e) => {
      const body = await e.response.json();
      alert(body.message);
      // sessionStorage.removeItem(ACCESS_TOKEN)
    },
  });
};

export const useLogoutMutation = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["sign-out"],
    mutationFn: async () => {
      return ky.post("auth/logout").json();
    },
    onSuccess: () => {
      queryClient.setQueryData(["auth"], null);
      // sessionStorage.removeItem(ACCESS_TOKEN)
    },
    onError: async (e) => {
      const body = await e.response.json();
      alert(body.message);
      // sessionStorage.removeItem(ACCESS_TOKEN)
    },
  });
};
