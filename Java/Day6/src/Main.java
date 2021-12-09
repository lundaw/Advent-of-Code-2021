import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.Arrays;

public class Main {
	public static void main(String[] args) {
		var input = readInput();
		System.out.printf("[Part 1]: %d\n", calculateForDays(input, 80));
		System.out.printf("[Part 2]: %d\n", calculateForDays(input, 256));
	}
	
	private static long[] readInput() {
		try (BufferedReader br = new BufferedReader(new FileReader("../inputs/day6.txt"))) {
			final var input = Arrays.stream(br.readLine().split(",")).mapToInt(Integer::parseInt).toArray();
			var generations = new long[9];
			Arrays.fill(generations, 0);
			for (var fish : input) {
				generations[fish]++;
			}
			
			return generations;
		} catch (FileNotFoundException e) {
			System.out.println("Input file was not found in the inputs folder.");
		} catch (IOException e) {
			System.out.println("An IO error occurred during reading the input file.");
		}
		
		throw new RuntimeException("Could not read input.");
	}
	
	private static long calculateForDays(final long[] generations, final int days) {
		var copyGenerations = generations.clone();
		for (int day = 0; day < days; day++) {
			long parents = copyGenerations[0];
			System.arraycopy(copyGenerations, 1, copyGenerations, 0, copyGenerations.length - 1);
			
			copyGenerations[6] += parents;
			copyGenerations[8] = parents;
		}
		
		return Arrays.stream(copyGenerations).sum();
	}
}
