using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tyoEngineEditor
{
    public class SQL
    {
        #region 物品
        /// <summary>
        /// 查所有物品
        /// </summary>
        public const string itemsSearch = "SELECT (@rownum:=@rownum+1) AS xh,a.name,itemsnum,a.itemsgrade,a.itemstype,a.itemsdroprate,a.id,a.onbodyimage,a.bagimage,a.dropimage FROM mapeditor.items a,(select @rownum:=0) t";

        public const string itemsInsert = "INSERT INTO mapeditor.items(name,onBodyImage,bagImage,dropImage,itemsNum,itemsGrade,itemsType,itemsdroprate)VALUES(?name,?onBodyImage,?bagImage,?dropImage,?itemsNum,?itemsGrade,?itemsType,?itemsdroprate)";

        public const string itemsUpdate = "UPDATE mapeditor.items SET name = ?name,onBodyImage = ?onBodyImage,bagImage = ?bagImage,dropImage = ?dropImage,itemsNum = ?itemsNum,itemsGrade=?itemsGrade,itemsType=?itemsType,itemsdroprate=?itemsdroprate WHERE ID = ?ID";

        public const string itemsDelete = "DELETE FROM mapeditor.items where id=?id";

        public const string itemsTypeSearch = "SELECT DISTINCT ifnull(itemsType,'未定义')itemsType FROM mapeditor.items ";

        public const string itemsSearchByType = "SELECT a.name,itemsnum,a.itemsgrade,a.itemstype,a.id,a.onbodyimage,a.bagimage,a.dropimage,if(ifnull(b.monstor_items_droprate,0)=0,a.itemsdroprate,b.monstor_items_droprate) rate "
            + "FROM mapeditor.items a LEFT JOIN mapeditor.monstor_items b  ON a.id=b.itemsid ";

        #endregion

        #region 怪物

        public const string monstorSearch = "SELECT name,num,monstorgrade,monstortype,id,"
            +" (select monstorimage from mapeditor.monstoranimation where monstorid=a.id limit 0,1)monstorimage "
            +" FROM mapeditor.monstor a ";

        public const string monstorDelete = "DELETE FROM mapeditor.monstor where id=?id";

        public const string monstorInsert = "INSERT INTO mapeditor.monstor(name,num,monstorgrade,monstorimage,monstortype) values(?name,?num,?monstorgrade,?monstorimage,?monstortype)";

        public const string monstorUpadte = "UPDATE mapeditor.monstor SET name=?name,num=?num,monstorimage=?monstorimage,monstorgrade=?monstorgrade,monstortype=?monstortype WHERE id=?id";

        public const string monstorDropItemsSearch = "SELECT i.ID,i.name,mi.monstor_items_droprate FROM mapeditor.monstor_items mi,mapeditor.items i where i.ID=mi.itemsID and mi.monstorID=?monstorID";

        public const string monstorDropItemsInsert = "INSERT INTO mapeditor.monstor_items(monstorID,itemsID,monstor_items_droprate) values(?monstorID,?itemsID,?monstor_items_droprate)";

        public const string monstorDropItemsUpdate = "UPDATE mapeditor.monstor_items SET monstor_items_droprate=?monstor_items_droprate "
            + "WHERE monstorID =?monstorID and itemsID = ?itemsID";

        public const string monstorDropItemsDelete = "DELETE FROM mapeditor.monstor_items WHERE monstorID =?monstorID and itemsID =?itemsID";

        public const string monstorImage = "select monstorimage from mapeditor.monstor where id=?id";

        public const string monstorAnimationSearch = "SELECT monstorimage,imageIndex FROM mapeditor.monstoranimation WHERE monstorid=?monstorid";

        public const string monstorAnimationInsert = "INSERT INTO mapeditor.monstoranimation(monstorid,monstorimage,imageindex) VALUES(?monstorid,?monstorimage,?imageindex)";

        public const string monstorAnimationDelete = "DELETE FROM mapeditor.monstoranimation WHERE monstorid=?monstorid";

        #endregion

        #region 技能

        public const string magicSearch = "SELECT id,magicname,type,''modifyType FROM mapeditor.magic WHERE type=?type";

        public const string magicDelete = "DELETE FROM mapeditor.magic WHERE id = ?id";

        public const string magicUpdate = "UPDATE mapeditor.magic SET type=?type,magicname=?magicname WHERE id=?id";

        public const string magicInsert = "INSERT INTO mapeditor.magic(type,magicname) values(?type,?magicname)";

        #endregion
    }
}
