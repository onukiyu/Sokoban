using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    //�����܂łɂ����鎞��
    private float timeTaken = 0.2f;
    //�o�ߎ���
    private float timeEraspeed;
    //�ړI�n
    private Vector3 destination;
    //�o���n
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        //�ړI�n�E�o���n�����ݒn�ŏ�����
        destination = transform.position;
        origin = destination;
    }

    public void MoveTo(Vector3 newDestination)
    {
        //�o�ߎ��Ԃ�������
        timeEraspeed = 0;
        //�ړ����̉\��������̂ŁA���ݒn��postion�ɑO��ړ��̖ړI�n����
        origin = destination;
        transform.position = origin;
        //�V�����ړI�n����
        destination = newDestination;
    }

    // Update is called once per frame
    void Update()
    {
        //�ړI�n�ɓ������Ă����珈�����Ȃ�
        if(origin == destination) { return; }
        //���Ԍo�߂����Z
        timeEraspeed += Time.deltaTime;
        //���Ԍo�߂��������Ԃ̉��������Z�o
        float timerate = timeEraspeed / timeTaken;
        //�������Ԃ𒴂���悤�ł���Ύ��s�������ԑ����Ɋۂ߂�B
        if(timerate > 1) { timerate = 1; }
        //�C�[�W���O�p�v�Z�i���j�A�j
        float easing = timerate;
        //���W���Z�o
        Vector3 currentPosition = Vector3.Lerp(origin, destination, easing);
        //�Z�o�������W��position�ɑ��
        transform.position = currentPosition;
    }
}
