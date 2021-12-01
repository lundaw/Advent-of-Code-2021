import 'dart:io';

main() async {
  final File input = new File("../inputs/day1.txt");
  final List<int> data = (await input.readAsLines().then((value) => value.map((e) => int.parse(e)))).toList();
  
  final int incrementsPart1 = calculalatePart1(data);
  print("[Part 1]: $incrementsPart1");

  final int incrementsPart2 = calculalatePart2(data);
  print("[Part 2]: $incrementsPart2");
}

int calculalatePart1(List<int> input) {
  int counter = 0;

  for (var i = 1; i < input.length; i++) {
    if (input[i - 1] < input[i]) {
      counter++;
    }
  }

  return counter;
}

int calculalatePart2(List<int> input) {
  int counter = 0;

  for (var i = 0; i < input.length - 3; i++) {
    final int firstWindow = input.skip(i).take(3).reduce((sum, element) => sum + element);
    final int secondWindow = input.skip(i + 1).take(3).reduce((sum, element) => sum + element);

    if (firstWindow < secondWindow) {
      counter++;
    }
  }

  return counter;
}