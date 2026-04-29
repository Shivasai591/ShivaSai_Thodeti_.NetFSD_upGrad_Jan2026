const {
  calculatePercentage,
  calculateGrade,
  isPass
} = require("../js/quiz");

test("calculates percentage correctly", () => {
  expect(calculatePercentage(4,5)).toBe(80);
});

test("returns correct grade", () => {
  expect(calculateGrade(85)).toBe("A");
});

test("determines pass correctly", () => {
  expect(isPass("B")).toBe(true);
});