import { useQueryClient } from "@tanstack/react-query";
import { useEffect } from "react";

import { useAuthQuery } from "@/api/auth.api";
import type { SafeUser } from "@/interfaces/users.interface";
import { router } from "@/lib/router";

type AuthData =
  | { user: null; status: "PENDING" }
  | { user: null; status: "UNAUTHENTICATED" }
  | { user: SafeUser; status: "AUTHENTICATED" };

function useAuth(): AuthData {
  const authQuery = useAuthQuery();

  const queryClient = useQueryClient();

  useEffect(() => {
    router.invalidate();
  }, [authQuery.data]);

  useEffect(() => {
    if (authQuery.error === null) return;
    queryClient.setQueryData(["auth"], null);
  }, [authQuery.error, queryClient]);

  if (authQuery.isPending) {
    return { user: null, status: "PENDING" };
  }

  if (!authQuery.data) {
    return { user: null, status: "UNAUTHENTICATED" };
  }

  return { user: authQuery.data, status: "AUTHENTICATED" };
}

export { useAuth };
export type { AuthData };
