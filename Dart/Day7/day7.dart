import 'dart:io';
import 'dart:math';

main() async {
  var input = await (new File("../../inputs/day7.txt")).readAsLines();
  var crabs = input.first.split(',').map((e) => int.parse(e)).toList();

  var costs = <int>[0xFFFFFFFF, 0xFFFFFFFF];
  int minPosition = crabs.reduce(min);
  int maxPosition = crabs.reduce(max);

  for (int i = minPosition; i < maxPosition; i++) {
    int sumFirst = crabs.fold<int>(0, (acc, e) => acc + (i - e).abs());
    if (sumFirst < costs[0]) {
      costs[0] = sumFirst;
    }

    int sumSecond = crabs.map((e) => (i - e).abs()).map((e) => (e * e + e) ~/ 2).fold<int>(0, (acc, e) => acc + e);
    if (sumSecond < costs[1]) {
      costs[1] = sumSecond;
    }
  }

  print("[Part 1]: ${costs[0]}");
  print("[Part 2]: ${costs[1]}");
}