package Day5;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.regex.Pattern;

public class Main {
	private final static int[][] field = new int[1000][1000];
	
	public static void main(String[] args) {
		var input = readInput();
		
		calculatePart1(input);
		System.out.printf("[Part 1]: %d\n", Arrays.stream(field).flatMapToInt(Arrays::stream).filter(v -> v >= 2).count());
		
		calculatePart2(input);
		System.out.printf("[Part 2]: %d\n", Arrays.stream(field).flatMapToInt(Arrays::stream).filter(v -> v >= 2).count());
	}
	
	private static List<int[]> readInput() {
		try (BufferedReader br = new BufferedReader(new FileReader("../inputs/day5.txt"))) {
			var pattern = Pattern.compile("^(\\d+),(\\d+) -> (\\d+),(\\d+)$");
			List<int[]> positions = new ArrayList<>();
			String line;
			while ((line = br.readLine()) != null) {
				var m = pattern.matcher(line.trim());
				if (!m.find())
					throw new RuntimeException("Input line was mismatched. Input was: " + line);
				
				int[] coordinate = new int[]{
					Integer.parseInt(m.group(1)),
					Integer.parseInt(m.group(2)),
					Integer.parseInt(m.group(3)),
					Integer.parseInt(m.group(4)),
				};
				positions.add(coordinate);
			}
			
			return positions;
		} catch (FileNotFoundException e) {
			System.out.println("Input file was not found in the inputs folder.");
		} catch (IOException e) {
			System.out.println("An IO error occurred during reading the input file.");
		}
		
		throw new RuntimeException("Could not read input.");
	}
	
	private static void calculatePart1(final List<int[]> data) {
		for (final int[] vent : data) {
			var start = vent[0] == vent[2] ? Math.min(vent[1], vent[3]) : Math.min(vent[0], vent[2]);
			var end = vent[0] == vent[2] ? Math.max(vent[1], vent[3]) : Math.max(vent[0], vent[2]);
			if (vent[0] == vent[2]) {
				for (int i = start; i <= end; i++)
					field[vent[0]][i]++;
			} else if (vent[1] == vent[3]) {
				for (int i = start; i <= end; i++)
					field[i][vent[1]]++;
			}
		}
	}
	
	private static void calculatePart2(final List<int[]> data) {
		for (final int[] vent : data) {
			if (vent[0] == vent[2] || vent[1] == vent[3])
				continue;
			
			int m = (vent[1] - vent[3]) / (vent[0] - vent[2]);
			var start = vent[0] < vent[2] ? 0 : 2;
			var end = vent[0] < vent[2] ? 2 : 0;
			if (m == 1) {
				for (int i = vent[start], j = vent[start + 1]; i <= vent[end]; i++, j++)
					field[i][j]++;
			}
			else {
				for (int i = vent[start], j = vent[start + 1]; i <= vent[end]; i++, j--)
					field[i][j]++;
			}
		}
	}
}
