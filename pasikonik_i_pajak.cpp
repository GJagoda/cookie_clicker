#include <iostream>

char ch_menu();
char* field(int n);
void values(int* n, int* p, int* x);
void game(char* field, int size, int p, int x);
void writeField(char* field, int size);
bool isWon(char* field, int size,int);
void playerTurn(char* field, int size, int p);
void computerTurn(char* field, int size, int x);


int main()
{
    int n, p, x;
    while (true) {
        if (ch_menu() == 'x')break;
        values(&n, &p, &x);
        game(field(n), n, p, x);
    }
    return 0;
}

char ch_menu()
{
    char ch;
    std::cout << "Press \"x\" to end program" << std::endl;
    std::cout << "Press any key to run program" << std::endl;
    std::cin >> ch;
    return ch;
}

void values(int* n, int* p, int* x) {
    int n1, p1, x1;

    do {
        std::cout << "Set number of fields" << std::endl;
        std::cin >> n1;
        std::cout << "Set number of fields that grasshopper can jump over" << std::endl;
        std::cin >> p1;
        std::cout << "Set number of fields that spider can cover in one turn" << std::endl;
        std::cin >> x1;
    } while (!(n1 > 0 && p1 > 0 && x1 > 0 && p1 < n1 && x1 < n1));
    *n = n1;
    *p = p1;
    *x = x1;
}

char* field(int n) {
    char* array = new char[n];
    for (int i = 0; i < n; i++) {
        array[i] = 'o';
    }
    return array;
}

void game(char* field, int size, int p, int x) {
    //field - field of the game
    // size - size of field
    // p - grasshopper can jump
    // x - spider can cover
    while (!isWon(field, size,p)) {
        playerTurn(field, size, x);
        computerTurn(field, size, p);
    }
    delete[] field;
}

void writeField(char* field, int size) {
    std::cout << '_';
    for (int i = 0; i < size; i++) {
        std::cout << field[i];
    }
    std::cout << '_' << std::endl;
}

bool isWon(char* field, int size, int horse_jump) {
    /*bool a = true;*/
    int horse_pos = -1;
    for (int i = 0; i < size; i++){
        if (field[i] == 'p') {
            horse_pos = i;

        }
    }
    for (int j = horse_pos + horse_jump; j > horse_pos; j--) {
        if (field[j] == 'o') {
            return false;
        }
    }

    return true;
}
void playerTurn(char* field, int size, int p) {
    for (int i = 0; i < p; i++) {
        int position = 0;
        // showing board
        
        std::cout << "Choose spider's field (0-" << size - 1 << "): ";
        std::cin >> position;

        if (position < 0 || position >= size || field[position] == 'x' || field[position] == 'p') {
            std::cout << "This field cannot be chosen. Try again." << std::endl;
            i--;
        }
        else {
            field[position] = 'x';
        }
    }
    writeField(field, size);
}
void computerTurn(char* field, int size, int x) {
    int maxJump = x;
    int horsePosition = -1;


    //pozycja konika
    for (int i = 0; i < size; i++) {
        if (field[i] == 'p') {
            horsePosition = i;
            break;
        }
        horsePosition = -1;
    }

    //zmiana pozycji
    for (int i = horsePosition + maxJump; i >= horsePosition && i!=-1; i--) {
        if (field[i] == 'o') {
            field[i] = 'p';
            break;
        }
    }
    field[horsePosition] == 'o';

    std::cout << "Horse turn:";
    writeField(field, size);
}