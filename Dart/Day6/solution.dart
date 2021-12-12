class Solution {
  var _lanternfish = {
    0: 0, 1: 0, 2: 0, 3: 0, 4: 0, 5: 0, 6: 0, 7: 0, 8: 0
  };

  Solution(final List<String> input) {
    input.first.split(',').map((e) => int.parse(e)).forEach((element) {
      _lanternfish[element] = _lanternfish[element]! + 1;
    });
  }

  int calculateForDays(final int days) {
    var copyLanternfish = Map.from(_lanternfish);
    for (int day = 0; day < days; day++) {
      int parents = copyLanternfish[0];
      for (int n = 1; n < copyLanternfish.length; n++) {
        copyLanternfish[n - 1] = copyLanternfish[n];
      }

      copyLanternfish[6] += parents;
      copyLanternfish[8] = parents;
    }

    return copyLanternfish.entries
      .fold(0, (previousValue, element) => previousValue + element.value as int);
  }
}