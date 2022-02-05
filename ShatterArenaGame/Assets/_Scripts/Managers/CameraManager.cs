using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraManager : Singleton<CameraManager> {

    #region Variables
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    [SerializeField]
    private CinemachineVirtualCamera vcam_noDamping;
    [SerializeField]
    private Camera minMapCam;
    #endregion

    public void SizeMinMapCam() {
        minMapCam.orthographicSize = Player.Instance.level + 1;
    }

    public void SelectVCamBasic() {
        StartCoroutine(nameof(ResetDampCoRoutine));
        vcam.Priority = 10;
        vcam_noDamping.Priority = 1;
    }
    public void SelectVCamNoDamping() {
        vcam.Priority = 1;
        vcam_noDamping.Priority = 10;
    }

    private IEnumerator ResetDampCoRoutine() {
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YawDamping = 0;
        yield return new WaitForSeconds(0.1f);
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_YawDamping = 12;
    }

    public void SetFollowAndLookAt(Transform go) {
        vcam.Follow = go;
        vcam.LookAt = go;
        vcam_noDamping.Follow = go;
        vcam_noDamping.LookAt = go;
    }
}
