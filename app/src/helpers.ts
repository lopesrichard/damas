export const variables = (element: HTMLElement, variables: object) => {
  for (const [key, value] of Object.entries(variables)) {
    element.style.setProperty(`--${key}`, value);
  }
};
