#include <iostream>
using namespace std;

struct Weight {
	int W[3];	/*�s���v����*/
	char sign;
};


int main()
{
	int s[3];/*��l�v��*/
	int sum = 0;/*�ۥ[�᪺���G*/
	int count = 0;/*���T������*/
	int cycle = 1;
	struct Weight I[4];/*�v�����ƶq*/

	cout << "��l�v��:" << endl;
	for (int i = 0; i < 3; i++)
	{
		cin >> s[i];
	}
	cout <<endl<< "�w���v��: ";
	for (int i = 0; i < 4; i++)/*��J�v������*/
	{
		cout << endl << "I" << i + 1 << ": "<<endl;
		for (int j = 0; j < 3; j++)
		{
			cin >> I[i].W[j];
		}
		cin >> I[i].sign;
	}
	cout << "        Chosen Instance  Current Weight Vector" << endl;
	cout << "Cycle 0       (" << s[0] << "," << s[1] << "," << s[2] << ")" << endl;
	while (count != 4)/*���T���Ƭ��F��4���N�~��j��*/
	{
		count = 0;
		for (int i = 0; i < 4; i++) 
		{
			sum = 0;
			for (int j = 0; j < 3; j++)
			{
				sum += s[j] * I[i].W[j];
			}
			if (sum <= 0 && I[i].sign == '+')/*�p�G�Ÿ��O����,���G�p�󵥩�0*/
			{
				for (int j = 0; j < 3; j++)
				{
					s[j] += I[i].W[j];
				}
			}
			else if (sum >= 0 && I[i].sign == '-')/*�p�G�Ÿ��O�t��,���G�j�󵥩�0*/
			{
				for (int j = 0; j < 3; j++)
				{
					s[j] -= I[i].W[j];
				}
			}
			else
			{
				count++;/*���T����+1*/
			}
			if (count != 4)/*�p�G������4����,�|�@���j�黼�Wcycle*/
			{
				cout << "Cycle " << cycle << "  (" << I[i].W[0] << "," << I[i].W[1] << "," << I[i].W[2] << "," << I[i].sign << ")";
				cout << "     (" << s[0] << "," << s[1] << "," << s[2] << ")" << endl;
				cout << endl;
				cycle++;
			}
		}
	}
	cout << "���׬�:" << "(" << s[0] << "," << s[1] << "," << s[2] << ")" << endl;
	cout << endl;
	system("pause");		
}