import 'dart:io';

import 'solution.dart';

main() async {
  final List<String> input = await (new File("../../inputs/day6.txt")).readAsLines();
  Solution solution = new Solution(input);

  final int resultPart1 = solution.calculateForDays(80);
  print("[Part 1]: $resultPart1");

  final int resultPart2 = solution.calculateForDays(256);
  print("[Part 2]: $resultPart2");
}