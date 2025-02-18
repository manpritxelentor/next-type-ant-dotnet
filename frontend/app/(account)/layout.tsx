import React from "react";
import { StoreProvider } from "../storeProvider";

export default function AccountLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <>
      <StoreProvider>
        <div>{children}</div>
      </StoreProvider>
    </>
  );
}
