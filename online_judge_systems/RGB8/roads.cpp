#include <iostream>

using namespace std;

int main(){
	
	int n, i, j, count = 0;
	cin >> n;
	int A[n][n];
	
	for (i = 0; i < n; i++)
		for (j = 0; j < n; j++)
			cin >> A[i][j];
			
	for (i = 0; i < n; i++)
		for (j = i + 1; j < n; j++)
			count += A[i][j];
			
	cout << count << endl;
	return 0;
}
