<?php
namespace App\Controller;

use App\Controller\AppController;

/**
 * Ranktable Controller
 *
 * @property \App\Model\Table\RanktableTable $Ranktable
 *
 * @method \App\Model\Entity\Ranktable[]|\Cake\Datasource\ResultSetInterface paginate($object = null, array $settings = [])
 */
class RanktableController extends AppController
{

    /**
     * Index method
     *
     * @return \Cake\Http\Response|void
     */
    public function index()
    {
        $ranktable = $this->paginate($this->Ranktable);

        $this->set(compact('ranktable'));
    }

    /**
     * View method
     *
     * @param string|null $id Ranktable id.
     * @return \Cake\Http\Response|void
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function view($id = null)
    {
        $ranktable = $this->Ranktable->get($id, [
            'contain' => []
        ]);

        $this->set('ranktable', $ranktable);
    }

    /**
     * Add method
     *
     * @return \Cake\Http\Response|null Redirects on successful add, renders view otherwise.
     */
    public function add()
    {
        $ranktable = $this->Ranktable->newEntity();
        if ($this->request->is('post')) {
            $ranktable = $this->Ranktable->patchEntity($ranktable, $this->request->getData());
            if ($this->Ranktable->save($ranktable)) {
                $this->Flash->success(__('The ranktable has been saved.'));

                return $this->redirect(['action' => 'index']);
            }
            $this->Flash->error(__('The ranktable could not be saved. Please, try again.'));
        }
        $this->set(compact('ranktable'));
    }

    /**
     * Edit method
     *
     * @param string|null $id Ranktable id.
     * @return \Cake\Http\Response|null Redirects on successful edit, renders view otherwise.
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function edit($id = null)
    {
        $ranktable = $this->Ranktable->get($id, [
            'contain' => []
        ]);
        if ($this->request->is(['patch', 'post', 'put'])) {
            $ranktable = $this->Ranktable->patchEntity($ranktable, $this->request->getData());
            if ($this->Ranktable->save($ranktable)) {
                $this->Flash->success(__('The ranktable has been saved.'));

                return $this->redirect(['action' => 'index']);
            }
            $this->Flash->error(__('The ranktable could not be saved. Please, try again.'));
        }
        $this->set(compact('ranktable'));
    }

    /**
     * Delete method
     *
     * @param string|null $id Ranktable id.
     * @return \Cake\Http\Response|null Redirects to index.
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function delete($id = null)
    {
        $this->request->allowMethod(['post', 'delete']);
        $ranktable = $this->Ranktable->get($id);
        if ($this->Ranktable->delete($ranktable)) {
            $this->Flash->success(__('The ranktable has been deleted.'));
        } else {
            $this->Flash->error(__('The ranktable could not be deleted. Please, try again.'));
        }

        return $this->redirect(['action' => 'index']);
    }

    /*
    リクエストURL
    http://localhost/rankproject/Ranktable/getRankings
    リクエストパラメータ
    無し
    レスポンスコード
    ランキングデータリスト
    [{"id":2,"name":"\u7530\u4e2d","time":200,"date":"2019-03-20T18:47:48+00:00"},{"id":1,"name":"\u4f50\u85e4","time":100,"date":"2019-03-20T18:47:48+00:00"}]
    */
	//ランキングデータリストを取得する。
	public function getRankings()
	{
        error_log("getRankings()");
        // これを行う事で対になるテンプレート(.tpl)が不要となる
		$this->autoRender = false;
		
		//テーブルからランキングリストをとってくる
        $query = $this->Ranktable->find("all");

        //クエリー処理を行う。
        $query->order(['time'=>'DESC']);   //降順
        $query->limit(5);                  //取得件数を5件までに絞る
		
		//クエリを実行してarrayにデータを格納
		$json_array = json_encode($query);

		// $json_array の内容を出力
		echo $json_array;
    }

    /*
    リクエストURL
    http://localhost/rankproject/Ranktable/setRanking
    リクエストパラメータ
    name  varchar
    time int
    レスポンスコード
    */
    //ランキングデータ単体をセットする。
	public function setRanking()
	{
		$this->autoRender = false;

        //POST パラメータを取得
        //nameとmessageをPOSTで受け取る。
        $name = "";
        if(isset($this->request->data['name']))
        {
            $name = $this->request->data['name'];
            error_log($name);
        }

        $time = 0;
        if(isset($this->request->data['time']))
        {
            $time = $this->request->data['time'];
            error_log($time);
        }
        //$postName   = $this->request->data["name"];
        //$postTime  = $this->request->data["time"];

        $date = array("Name"=>$name,"Time"=>$time);
            //"date"=>date("Y/m/d H:i:s")

        //テーブルにレコードを追加
        $ranktable = $this->Ranktable->newEntity();
        $ranktable = $this->Ranktable->patchEntity($ranktable,$date);

        if($this->Ranktable->save($ranktable)){
            echo "success";   //成功s
        }else{
            echo "failed";   //失敗
        }
    }
}
