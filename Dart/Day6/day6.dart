import 'dart:io';

main() async {
  var lanternfish = {
    0: 0, 1: 0, 2: 0, 3: 0, 4: 0, 5: 0, 6: 0, 7: 0, 8: 0
  };
  final List<String> input = await (new File("../../inputs/day6.txt")).readAsLines();
  input.first.split(',').map((e) => int.parse(e)).forEach((e) { lanternfish[e] = lanternfish[e]! + 1; });

  for (int day = 0; day < 256; day++) {
    int parents = lanternfish[0]!;
    for (int n = 1; n < lanternfish.length; n++) {
      lanternfish.update(n - 1, (_) => lanternfish[n]!);
    }

    lanternfish.update(6, (val) => val + parents);
    lanternfish.update(8, (_) => parents);

    if (day == 80) {
      var count = lanternfish.entries
        .fold<int>(0, (acc, e) => acc + e.value);
      print("[Part 1]: $count");
    }
  }

  var finalCount = lanternfish.entries
    .fold<int>(0, (acc, e) => acc + e.value);
  print("[Part 2]: $finalCount");
}