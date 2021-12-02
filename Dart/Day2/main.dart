import 'dart:io';

main() async {
  final File input = new File("../../inputs/day2.txt");
  final List<String> data = await input.readAsLines();

  final int resultPart1 = calculalatePart1(data);
  print("[Part 1]: $resultPart1");

  final int resultPart2 = calculalatePart2(data);
  print("[Part 2]: $resultPart2");
}

int calculalatePart1(List<String> input) {
  int position = 0;
  int depth = 0;

  input.forEach((element) {
    final line = element.split(" ");
    final String command = line[0];
    final int units = int.parse(line[1]);

    switch (command) {
      case "forward":
        position += units;
        break;
      case "down":
        depth += units;
        break;
      case "up":
        depth -= units;
        break;
    }
  });

  return position * depth;
}

int calculalatePart2(List<String> input) {
  int position = 0;
  int depth = 0;
  int aim = 0;

  input.forEach((element) {
    final line = element.split(" ");
    final String command = line[0];
    final int units = int.parse(line[1]);

    switch (command) {
      case "forward":
        position += units;
        depth += aim * units;
        break;
      case "down":
        aim += units;
        break;
      case "up":
        aim -= units;
        break;
    }
  });

  return position * depth;
}