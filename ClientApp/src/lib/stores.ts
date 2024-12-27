import { writable } from "svelte/store";
import type { User } from "./user";

const user: User | null = null;

export const userStore = writable(user);