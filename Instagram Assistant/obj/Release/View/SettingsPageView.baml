   M S B A M L   `   `   `  ����   Instagram Assistant/    )Instagram_Assistant.View.SettingsPageView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources  ��%    .���� $
��Auto=�$��Visible=���.  .���� $��	20,0,20,0q�.+��� $��Center=�$��SETTINGS��$��30(�
�������   TextBlockH1Style#���   �� $��Center=�$��0,0,0,1q�$��	0,10,0,10q�
���������   TextBlockH2Style#���  $��Bottom=�$��General����  �� Orientation$ Vertical=�$��20,0,0,0q�.+��� $ 
Horizontal=��� $ 
Horizontal=�$��	20,10,0,0q�.+���   TextBlockH3Style#���  $��Dont do anything from ��$��Bottom=�$��0,0,10,0q��� $��20(�  MyWaterMarkStyle#���  $��0q�$��30������ *NotWorkFromHour+ �� UpdateSourceTrigger$ PropertyChanged=��� #���    Regular#���  $��until��$��Bottom=�$��	10,0,10,0q��� $��20(�#���  $��0q�$��30������ *NotWorkToHour+$ PropertyChanged=��� $ 
Horizontal=�$��	20,10,0,0q�.+��� #���  $��Bottom=�$3��-Rest time after 1 hour working or max actions��$��0,0,10,0q��� $��20(�#���  $��0q�$��40�� �� TextAlignment$ Center=����� *RestTimeMinutes+$ PropertyChanged=��� #���  $��Bottom=�#���  $��minutes��$��10,0,0,0q��� $��Center=�$��0,0,0,1q�$��	0,20,0,20q�
��������� #���  $��Bottom=�$
��Like���� $ 
Horizontal=�.+��� $ Vertical=�$��20,0,0,0q�.+��� $ 
Horizontal=�$��Center=�$��0,5,0,0q�.+��� $��Delay��$��Center=�#���  $	��120���� -    ��Name$ delayslider��$	��300��  SliderStyle#���  $��Center=� � Minimum$ 1Q� � Maximum$ 60Q� � Value �� *DelaySliderValue+�� $��10,0,0,5q�$��20(�#���  $��Center=�$��0q�$��30������ *Value+ �� ElementName$ delayslider��	 �� Mode$	 TwoWay=�
 �� StringFormat$
 F0���� $	��sec��$��Center=�#���  �� $ 
Horizontal=�$��Center=�$��0,5,0,0q�.+��� $��Random Delay��$��Center=�#���  $	��120���� -   $ randomdelayslider��$	��300��#���  $��Center=�$ 0Q�$ 30Q� �� *RandomDelaySliderValue+�� $��10,0,0,5q�$��20(�#���  $��Center=�$��0q�$��30������ *Value+$ randomdelayslider��$	 TwoWay=�$
 F0���� $	��sec��$��Center=�#���  �� $ 
Horizontal=�$��Center=�$��0,5,0,0q�.+��� $
��Skip��$��Center=�#���  $	��120���� -   $ 
skipslider��$	��300��#���  $��Center=�$ 1Q�$ 70Q� �� *SkipSliderValue+�� $��10,0,0,5q�$��20(�#���  $��Center=�$��0q�$��30������ *Value+$ 
skipslider��$	 TwoWay=�$
 F0���� $��%��  Big#���  $��Center=�  TextBlockMainStyle#���  �� $ 
Horizontal=�$��0,10,0,0q�.+��� #���  $��Maximum likes per hour��$��Bottom=�$��0,0,10,0q��� $��20(�#���  $��0q�$��40������ *MaxLikesPerHour+$ PropertyChanged=��� $��80,0,0,0q�.+��� $��15(�
�������$��Center=�$��Average likes per hour���� $��60(�
�������$��Center=����� *AverageLikesCount+�� $��12(�
�������$��Center=�$#��60 likes per hour recommended���� $��Center=�$��0,0,0,1q�$��	0,20,0,20q�
��������� #���  $��Bottom=�$��Stories���� $ 
Horizontal=�.+��� $ Vertical=�$��20,0,0,0q�.+��� $ 
Horizontal=�$��Center=�$��0,5,0,0q�.+��� $��Delay��$��Center=�#���  $	��120���� -   $ delaystoriesslider��$	��300��#���  $��Center=�$ 5Q�$ 15Q� �� *DelayStoriesSliderValue+�� $��10,0,0,5q�$��20(�#���  $��Center=�$��0q�$��30������ *Value+$ delaystoriesslider��$	 TwoWay=�$
 F0���� $	��sec��$��Center=�#���  �� $ 
Horizontal=�$��Center=�$��0,5,0,0q�.+��� $��Random Delay��$��Center=�#���  $	��120���� -   $ randomdelaystoriesslider��$	��300��#���  $��Center=�$ 1Q�$ 8Q� �� *RandomDelayStoriesSliderValue+�� $��10,0,0,5q�$��20(�#���  $��Center=�$��0q�$��30������ *Value+$ randomdelaystoriesslider��$	 TwoWay=�$
 F0���� $	��sec��$��Center=�#���  �� $ 
Horizontal=�$��Center=�$��0,5,0,0q�.+��� $
��Skip��$��Center=�#���  $	��120���� -   $ skipstoriesslider��$	��300��#���  $��Center=�$ 1Q�$ 70Q� �� *SkipStorySliderValue+�� $��10,0,0,5q�$��20(�#���  $��Center=�$��0q�$��30������ *Value+$ skipstoriesslider��$	 TwoWay=�$
 F0���� $��%��$��Center=�#���  �� $ 
Horizontal=�$��0,10,0,0q�.+��� #���  $��Maximum stories per hour��$��Bottom=�$��0,0,10,0q��� $��20(�#���  $��0q�$��40������ *MaxStoriesPerHour+$ PropertyChanged=��� $��80,0,0,0q�.+��� $��15(�
�������$��Center=�$��Average stories per hour���� $��60(�
�������$��Center=����� *AverageStoriesCount+�� $��12(�
�������$��Center=�$��no limits for stories���� $��Center=�$��0,0,0,1q�$��	0,20,0,20q�
��������� #���  $��Bottom=�$��Unfollow���� $ 
Horizontal=�$��	20,5,0,10q�.+��� $ Vertical=�.+��� $ 
Horizontal=�$��	0,10,0,10q�.+��� $'��!Unfollow users that following you��$��	0,0,200,0q�$��15(�#���  ��   OrangeSwitchStyle#���   d� 	IsChecked �� *UnfollowIsChecked+�� $ 
Horizontal=�.+��� $��Delay��$��Center=�#���  $	��120���� -   $ delayunfollowslider��$	��300��#���  $��Center=�$ 30Q�$	 180Q� �� *DelayUnfollowSliderValue+�� $��	10,0,0,10q�$��20(�#���  $��Center=�$��0q�$��35������ *Value+$ delayunfollowslider��$	 TwoWay=�$
 F0���� $	��sec��$��3,0,0,4q�$��Center=�#���  �� $ 
Horizontal=�.+��� $��Random Delay��$��Center=�#���  $	��120���� -   $ randomdelayunfollowslider��$	��300��#���  $��Center=�$ 5Q�$ 20Q� �� * RandomDelayUnfollowSliderValue+�� $��10,0,0,5q�$��20(�#���  $��Center=�$��0q�$��30������ *Value+$ randomdelayunfollowslider��$	 TwoWay=�$
 F0���� $	��sec��$��Center=�#���  �� $ 
Horizontal=�$��0,10,0,0q�.+��� #���  $ ��Maximum unfollows per hour��$��Bottom=�$��0,0,10,0q��� $��20(�#���  $��0q�$��40������ *MaxUnfollowPerHour+$ PropertyChanged=��� $��80,0,0,0q�.+��� $��15(�
�������$��Center=�$��Average unfollow per hour���� $��60(�
�������$��Center=����� *AverageUnfollowCount+�� $��12(�
�������$��Center=�$&�� 30 unfollow per hour recommended���� $��Center=�$��0,0,0,1q�$��	0,20,0,20q�
��������� #���  $��Bottom=�$��Filter���� $ Vertical=�$��20,5,0,0q�.+��� $��0,0,0,10q�.+��� .+��� #���  $��
Stop Words���� $��0,3,0,3q�$ 
Horizontal=�.+��� $	��450������ *StopWordsPath+�� $��10,0,0,0q�$��50�� 	 MainButtonStyle#��� 	 ���� *SetStopWordsCommand+.��...��  
 TextBlockDiscriptionStyle#��� 
 $P��J*If one of those words exist in Bio or Full Name this user will not be add���� $��0,0,0,10q�.+��� .+��� #���  $)��#May be in bio or in Full Name words���� $��0,3,0,3q�$ 
Horizontal=�.+��� $	��450������ *AllowWordsPath+�� $��10,0,0,0q�$��50��#��� 	 ���� *SetGoWordsPathCommand+.��...�� 
�������$��13(�$��Italic$�#��� 
 $F��@*If one of those exist in Bio or Full Name this user will be add���� $��0,0,0,10q�.+��� .+��� #���  $"��May be in account name words���� $��0,3,0,3q�$ 
Horizontal=�.+��� $	��450������ *WordsInAccNamePath+�� $��10,0,0,0q�$��50��#��� 	 ���� *SetInNameWordsCommand+.��...�� 
�������$��13(�$��Italic$�#��� 
 $E��?*If one of those words exist in User Name this user will be add���� $��0,0,0,10q�.+��� .+��� #���  $��Save audience to���� $��0,3,0,3q�$ 
Horizontal=�.+��� $	��450������ *SaveAudiencePath+�� $��10,0,0,0q�$��50��#��� 	 ���� *SetAudienceCommand+.��...�� $��0,0,0,10q�.+��� .+��� $��0,3,0,3q�#���  $��Save filtered audience to���� $ 
Horizontal=�.+��� $	��450������ *SaveFilterAudiencePath+�� $��10,0,0,0q�$��50��#��� 	 ���� *SetFilterAudienceCommand+.��...�� .+��� .+��� #���  $��Save parametrs���� $��0,3,0,3q�$ 
Horizontal=�.+��� $
��Name��  CheckBoxStyle#���  $��0,0,20,0q�
�������$��16(� �� *
IsNameSave+�� $��Id��#���  $��0,0,20,0q�
�������$��16(� �� *
IsIdSave+�� $��Phone��#���  $��0,0,20,0q�
�������$��16(� �� *IsPhoneSave+�� $��Email��#���  
�������$��16(� �� *IsEmailSave+�� .+��� 
�������$��13(�$��Italic$�#��� 
 $B��<*Гser will not be saved if one of the parameters is missing���� #���  $��Bottom=�$��0,10,0,0q�$��Filter Parametrs���� $��20,5,0,0q�.+��� $ 
Horizontal=�$��0,0,0,10q�.+��� #���  $��Minimum followers��$��Center=�$��0,0,10,0q��� $��20(�#���  $��0q�$��40������ *MinFollowersCount+�� $ 
Horizontal=�$��0,0,0,10q�.+��� #���  $��Minimum media count��$��Center=�$��0,0,10,0q��� $��20(�#���  $��0q�$��40������ *MinMediaCount+�� $ 
Horizontal=�$��0,0,0,10q�.+��� #���  $��Account category��$��0,0,10,0q�$��Center=���   ComboBoxStyle#���  $��80�� �� SelectedIndex �� *AccountType+.@��� .��
Personal�� .��
Business�� .��Author�� $ 
Horizontal=�$��0,0,0,10q�.+��� $��Has Bio��#���  
�������$��16(�$��0,0,10,0q� �� *HasBioCheck+�� $��Has Hightlights��#���  
�������$��16(�$��0,0,10,0q� �� *
HasHLCheck+�� $��Has Profile Image��#���  
�������$��16(� �� *HasImageCheck+�� $ 
Horizontal=�$��0,0,0,10q�.+��� #���  $��
Categories��$��0,0,10,0q��� $	��300������ *CategoriesToCheck+