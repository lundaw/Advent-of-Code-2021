#include <stdio.h>
#include <stdlib.h>

int getLineCount(FILE *fp) {
    int val;
    int lineCount = 0;

    do {
        val = fgetc(fp);
        if (val == '\n' || val == EOF) {
            lineCount++;
        }
    } while (val != EOF);

    return lineCount;
}

void readInput(const char *inputFile, int **dataArray) {
    if (inputFile == NULL) {
        printf("Input file is NULL");
        return;
    }

    FILE *fp = fopen(inputFile, "r");
    if (!fp) {
        printf("Failed to open input file");
        return;
    }

    const int lineCount = getLineCount(fp);
    fseek(fp, 0, SEEK_SET);

    // Get the values
    int *data = (int *) malloc(lineCount * sizeof(int) + 1);
    *dataArray = data + 1;
    data[0] = lineCount;
    int i = 1;
    do {
        fscanf(fp, "%d\n", &data[i++]);
    } while (!feof(fp));

    fclose(fp);
}

unsigned int calculatePart1(const int *input) {
    unsigned int counter = 0;

    for (unsigned int i = 1; i < input[-1]; i++) {
        if (input[i - 1] < input[i]) {
            counter++;
        }
    }

    return counter;
}

unsigned int calculatePart2(const int *input) {
    unsigned int counter = 0;

    for (unsigned int i = 0; i < input[-1] - 3; i++) {
        const int firstWindow = input[i] + input[i + 1] + input[i + 2];
        const int secondWindow = input[i + 1] + input[i + 2] + input[i + 3];

        if (firstWindow < secondWindow) {
            counter++;
        }
    }

    return counter;
}

int main() {
    int *input = NULL;
    
    readInput("../../inputs/day1.txt", &input);
    printf("[Part 1]: %d\n", calculatePart1(input));
    printf("[Part 2]: %d\n", calculatePart2(input));

    free(input - 1);
    return 0;
}