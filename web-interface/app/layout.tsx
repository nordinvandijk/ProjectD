import React from "react";
import "./globals.css";

export const metadata = {
  title: "Driving sim data hub",
  description: "Portal for managing data points.",
};

type RootLayoutProps = { children: React.ReactNode };

const RootLayout = ({ children }: RootLayoutProps) => {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  );
};

export default RootLayout;
